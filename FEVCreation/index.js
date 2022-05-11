/*
Run 'npm start' in terminal then open localhost:3000

Enter values into each field and click 'Create Fish'
app.post should then create an xml file in /VATS_PROJECT/Assets/Resources/FEVs/
Values that were entered by user should be in correct tags in the new file

If you want to add new xml tags just add the input fields in views/index.ejs following the
same convention as the others and add the name of the input to userEntries.
(name attribute in <input> tag is how this file gets the value from the form)
*/

// Installed modules from node
const express = require('express')
const { promises: fs } = require('fs')

// Custom functions
const createXmlString = require('./helperFunctions/createXmlString')

// Where the FEVs are located
const FEV_DIR = '../VATS_PROJECT/Assets/Resources/FEVs/'

// Misc
const app = express()

// Allows us to use EJS
app.set("view engine", "ejs")
app.use(express.static("public"))

// Allows us to use app.post
app.use(express.urlencoded({extended:true}))

app.get('/', async function(req, res) {
  res.render('index')
})//Root

app.post('/', async function(req, res) {
  // Grabbing each value that user entered from views/index.ejs
  let userEntries = [
    'neighborRadius',
    'avoidRadius',
    'collisionLength',
    'minSpeed',
    'maxSpeed',
    'minSize',
    'maxSize',
    'minTemp',
    'maxTemp',
    'minDepth',
    'maxDepth',
    'lowerLimit',
    'upperLimit',
    'fishType',
    'modelUrl',
    'name', //This determines xml file name
    'scientificName',
    'type',
    'diet',
    'habitat',
    'range',
    'status'
  ]

  let xml = await createXmlString(req, userEntries, FEV_DIR)

  // XML file is now created in correct directory with correct contents
  fs.writeFile(`${FEV_DIR}${req.body.name}.xml`, xml)
  
  res.render('index')
})

app.listen(3000, () => {
  console.log('Expresss server running...')
  console.log('Open http://localhost:3000')
})
