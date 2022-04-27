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
  let xml = req.body.xml
  console.log("-------" + xml)
  let id  = await generateId(dir)
  xml += `<id>${id}</id>`
  xml += "</FEV>"
  const xmlDoc = parser.parseFromString(xml, 'application/xml')
  console.log(xml)
  console.log(xmlDoc)
})

app.listen(3000, () => {
    console.log("Expresss server running...");
  })