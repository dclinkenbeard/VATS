const express = require("express");
const { promises: fs } = require('fs');
const dir = '../VATS_PROJECT/Assets/Resources/FEVs/';

const app = express();

app.set("view engine", "ejs");
app.use(express.static("public"));

const generateId = async (dir) => {
  const files = await fs.readdir(dir) 
  id = (Math.floor(files.length / 2)) + 1
  return id 
}

app.get("/", async function(req, res) {
  res.render("index");
});//Root

app.post("/", async function(req, res) {
  
  
  // Creating XML file
  let parser = new DOMParser();
  let xml = '<?xml version="1.0" encoding="UTF-8" standalone="yes" ?>'
  xml += '<FEV>'
  xml += `<neighborRadius>${neighborRadius.value}</neighborRadius>`
  xml += `<avoidRadius>${avoidRadius.value}</avoidRadius>`
  xml += `<collisionLength>${collisionLength.value}</collisionLength>`
  xml += `<minSpeed>${minSpeed.value}</minSpeed>`
  xml += `<maxSpeed>${maxSpeed.value}</maxSpeed>`
  xml += `<minSize>${minSize.value}</minSize>`
  xml += `<maxSize>${maxSize.value}</maxSize>`
  xml += xmlMinTemp
  xml += xmlMaxTemp
  xml += xmlMinDepth
  xml += xmlMaxDepth
  xml += xmlLowerLimit
  xml += xmlUpperLimit
  xml += xmlFishType
  xml += xmlModelUrl
  xml += xmlName
  xml += xmlScientificName
  xml += xmlType
  xml += xmlDiet
  xml += xmlHabitat
  xml += xmlRange
  xml += xmlStatus
  let id = await generateId(dir)
  xml += `<id>${id}</id>`
  xml += "</FEV>"
  const xmlDoc = parser.parseFromString(xml, 'application/xml')
  console.log(xml)
  console.log(xmlDoc)
})

app.listen(3000, () => {
    console.log("Expresss server running...");
  })