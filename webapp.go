package main

import (
	"./graph"
	"./netlunsj"
	"log"
	"net/http"
)

func main() {
	g, example := graph.SetupGraph()
	netlunsj.Serve(g, example)
	err := http.ListenAndServe(":4040", nil)
	if err != nil {
		log.Fatal("ListenAndServe: ", err)
	}
}
