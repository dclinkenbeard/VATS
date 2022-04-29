const express = require("express");
// const bodyParser = require("body-parser")
const { promises: fs } = require('fs');
const dir = '../VATS_PROJECT/Assets/Resources/FEVs/';

// var urlencodedParser = bodyParser.urlencoded({ extended: false })


const app = express();

app.set("view engine", "ejs");
app.use(express.static("public"));
app.use(express.urlencoded({extended:true}));

const generateId = async (dir) => {
  const files = await fs.readdir(dir) 
  var id = (Math.floor(files.length / 2)) + 1
  return id 
}

app.get("/", async function(req, res) {
  res.render("index");
});//Root

app.post("/", async function(req, res) {
  let neighborRadius = req.body.neighborRadius
  let avoidRadius = req.body.avoidRadius
  let collisionLength = req.body.collisionLength
  let minSpeed = req.body.minSpeed
  let maxSpeed = req.body.maxSpeed
  let minSize = req.body.minSize
  let maxSize = req.body.maxSize
  let minTemp = req.body.minTemp
  let maxTemp = req.body.maxTemp
  let minDepth = req.body.minDepth
  let maxDepth = req.body.maxDepth
  let lowerLimit = req.body.lowerLimit
  let upperLimit = req.body.upperLimit
  let fishType = req.body.fishType
  let modeUrl = req.body.modeUrl
  let name = req.body.name
  let scientificName = req.body.scientificName
  let type = req.body.type
  let diet =  req.body.diet 
  let habitat = req.body.habitat
  let range = req.body.range
  let status = req.body.status
  
  // Creating XML file
  var DOMParser = require('xmldom').DOMParser;
  let parser = new DOMParser();

  let xml = '<?xml version="1.0" encoding="UTF-8" standalone="yes" ?>'
  xml += '<FEV>'
  xml += `<neighborRadius>${neighborRadius}</neighborRadius>`
  xml += `<avoidRadius>${avoidRadius}</avoidRadius>`
  xml += `<collisionLength>${collisionLength}</collisionLength>`
  xml += `<minSpeed>${minSpeed}</minSpeed>`
  xml += `<maxSpeed>${maxSpeed}</maxSpeed>`
  xml += `<minSize>${minSize}</minSize>`
  xml += `<maxSize>${maxSize}</maxSize>`
  xml += `<minTemp>${minTemp}</minTemp>`
  xml += `<maxTemp>${maxTemp}</maxTemp>`
  xml += `<minDepth>${minDepth}</minDepth>`
  xml += `<maxDepth>${maxDepth}</maxDepth>`
  xml += `<lowerLimit>${lowerLimit}</lowerLimit>`
  xml += `<upperLimit>${upperLimit}</upperLimit>`
  xml += `<fishType>${fishType}</fishType>`
  xml += `<modeUrl>${modeUrl}</modeUrl>`
  xml += `<name>${name}</name>`
  xml += `<scientificName>${scientificName}</scientificNames>`
  xml += `<type>${type}</type>`
  xml += `<diet>${diet}</diet>`
  xml += `<habitat>${habitat}</habitat>`
  xml += `<range>${range}</range>`
  xml += `<status>${status}</status>`
  let id = await generateId(dir)
  xml += `<id>${id}</id>`
  xml += "</FEV>"
  const xmlDoc = parser.parseFromString(xml, 'application/xml')
  console.log(xml)

  // XML file is now created in correct directory with correct contents
  fs.writeFile(`../VATS_PROJECT/Assets/Resources/FEVs/${name}.txt`, xml);
  
  console.log(id)
})

app.listen(3000, () => {
    console.log("Expresss server running...");
  })