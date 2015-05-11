package netlunsj

import (
	"../graph"
	"encoding/json"
	"fmt"
	"github.com/gorilla/mux"
	"io/ioutil"
	"log"
	"net/http"
)

var g *graph.Graph
var example *graph.Graph

func indexHandler(w http.ResponseWriter, r *http.Request) {
	http.ServeFile(w, r, "./public/html/index.html")
}

func overviewHandler(w http.ResponseWriter, r *http.Request) {
	http.ServeFile(w, r, "./public/html/overview.html")
}

func overviewGraphHandler(w http.ResponseWriter, r *http.Request) {
	// Turn graph into json
	b, err := json.Marshal(g)
	if err != nil {
		fmt.Println("error:", err)
	}

	// send to client
	w.Header().Set("Content-Type", "application/json")
	w.Write(b)
}

func imagesExampleHandler(w http.ResponseWriter, r *http.Request) {
	http.ServeFile(w, r, "./public/html/keggpictures.html")
}

func imagesExampleGraphHandler(w http.ResponseWriter, r *http.Request) {
	// Turn graph into json
	b, err := json.Marshal(example)
	if err != nil {
		fmt.Println("error:", err)
	}

	// send to client
	w.Header().Set("Content-Type", "application/json")
	w.Write(b)
}

func publicHandler(w http.ResponseWriter, r *http.Request) {
	vars := mux.Vars(r)
	folder := vars["folder"]
	file := vars["file"]
	base := "public/"
	filename := base + folder + "/" + file
	log.Println("Received request for: ", filename)

	f, err := ioutil.ReadFile(filename)
	if err != nil {
		w.Write([]byte("Could not find file " + filename))
	} else {
		w.Write(f)
	}
}

func Serve(foo *graph.Graph, bar *graph.Graph) {
	g = foo
	example = bar
	r := mux.NewRouter()

	r.HandleFunc("/", indexHandler)

	r.HandleFunc("/public/{folder}/{file}", publicHandler)
	r.HandleFunc("/overview", overviewHandler)
	r.HandleFunc("/overview-graph", overviewGraphHandler)
	r.HandleFunc("/images-example", imagesExampleHandler)
	r.HandleFunc("/images-example-graph", imagesExampleGraphHandler)

	http.Handle("/", r)
}
