package graph

import (
	"fmt"
	"github.com/Knudah/kvik/kegg"
	"image"
	"image/png"
	"log"
	"net/http"
	"os"
	"strings"
)

type Graph struct {
	Nodes []Node
	Edges []Edge
}

type Node struct {
	Id string `json:"id"`
}

type Edge struct {
	Source int `json:"source"`
	Target int `json:"target"`
}

func (g Graph) hasNode(a Node) bool {
	for _, n := range g.Nodes {
		if a.Id == n.Id {
			return true
		}
	}
	return false
}

func (g Graph) getIndex(a string) int {
	for i, n := range g.Nodes {
		if a == n.Id {
			return i
		}
	}
	return -1
}

func checkIfImageExists(filename string) bool {
	if _, err := os.Stat("./public/img/" + filename + ".png"); os.IsNotExist(err) {
		return false
	}
	return true
}

func GetImage(keggId string) image.Image {
	imgurl := "http://rest.kegg.jp/get/" + keggId + "/image"
	resp, err := http.Get(imgurl)
	if err != nil {
		log.Println("Image could not be downloaded ", err)
		return nil
	} else {
		img, err := png.Decode(resp.Body)

		if err != nil {
			log.Panic("Image could not be decoded ", err)
			return nil
		}
		return img
	}
}

func StoreImage(path, filename string, image image.Image) error {

	err := os.MkdirAll(path, 0755)
	if err != nil {
		return err
	}

	fn := path + "/" + filename
	file, err := os.Create(fn)
	if err != nil {
		return err
	}

	return png.Encode(file, image)

}

func SetupGraph() (*Graph, *Graph) {
	var g *Graph
	var example *Graph
	var keggpathway []*kegg.KeggPathway
	var source int
	var target int
	testpathway := "hsa05219"
	g = new(Graph)
	example = new(Graph)
	pathway := kegg.GetAllHumanPathways()

	// Build up the keggpathway with all the human pathways
	for i := range pathway {
		node := Node{pathway[i].Id}
		if !g.hasNode(node) {
			g.Nodes = append(g.Nodes, node)
		}
		if pathway[i].Id == testpathway {
			example.Nodes = append(example.Nodes, node)
			if checkIfImageExists(pathway[i].Id) != true {
				img := kegg.GetImage(pathway[i].Id)
				if img == nil {
					log.Panic("Failed to retrieve image for pathway: ", pathway[i].Id)
				}
				err := StoreImage("./public/img/", pathway[i].Id+".png", img)
				if err != nil {
					log.Panic("Failed to store image for pathway: ", pathway[i].Id)
				}
			}
		}
		keggpathway = append(keggpathway, kegg.NewKeggPathway(pathway[i].Id))
	}
	fmt.Println("Human pathways: ", len(pathway))
	// Iterate through all the entries for the human pathways
	for i := range pathway {
		for j := range keggpathway[i].Entries {
			pathwayentry := strings.TrimPrefix(keggpathway[i].Entries[j].Name, "path:")
			// Check if the entry is a map and its not itself
			if keggpathway[i].Entries[j].Type == "map" && pathwayentry != pathway[i].Id {
				if source = g.getIndex(pathway[i].Id); source == -1 {
					log.Println("Should never happen because of self")
				}
				// If the edge node is not a human pathway, we need to add that pathway
				if target = g.getIndex(pathwayentry); target == -1 {
					node := Node{pathwayentry}
					if !g.hasNode(node) {
						g.Nodes = append(g.Nodes, node)
					}
					target = g.getIndex(pathwayentry)
				}
				if pathway[i].Id == testpathway {

					node := Node{pathwayentry}
					if !example.hasNode(node) {
						example.Nodes = append(example.Nodes, node)
						if checkIfImageExists(pathwayentry) != true {
							fmt.Println("Getting img for: ", pathwayentry)
							img := kegg.GetImage(pathwayentry)
							if img == nil {
								log.Panic("Failed to retrieve image for pathway: ", pathwayentry)
							}
							err := StoreImage("./public/img/", pathwayentry+".png", img)
							if err != nil {
								log.Panic("Failed to store image for pathway: ", pathwayentry)
							}
						}
					}
					e := Edge{0, example.getIndex(pathwayentry)}
					example.Edges = append(example.Edges, e)
				}
				e := Edge{source, target}
				g.Edges = append(g.Edges, e)
				// linkedpathway[pathway[i].Id] = append(linkedpathway[pathway[i].Id], strings.TrimPrefix(keggpathway[i].Entries[j].Name, "path:"))
				// fmt.Println(strings.TrimPrefix(keggpathway[i].Entries[j].Name, "path:"))
			}
		}
	}
	fmt.Println("Total nodes: ", len(g.Nodes))
	fmt.Println("Total edges: ", len(g.Edges))
	return g, example
}
