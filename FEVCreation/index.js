/*
Run 'npm start' in terminal then open localhost:3000
Enter values into each input and click 'Create Fish'
app.post should then execute and create an xml file in /VATS_PROJECT/Assets/Resources/FEVs/
Values that were entered by user should be in correct tags in the new file

If you  waant to add new xml tags just add the input fields in index.js following the
same convention as the others and add the name of the input to userEntries.

STRETCH GOALS:
Create Unity scene with a button that will start the node server open localhost:3000
*/

// Installed modules from node
const express = require('express')
const { promises: fs } = require('fs')

// Custom functions
const createXmlString = require('./helperFunctions/createXmlString')

// Where the FEVs are located
const dir = '../VATS_PROJECT/Assets/Resources/FEVs/'

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
    'name',
    'scientificName',
    'type',
    'diet',
    'habitat',
    'range',
    'status',
  ]

  let xml = await createXmlString(req, userEntries, dir)
  let indexOfName = userEntries.indexOf('name')

  // XML file is now created in correct directory with correct contents
  fs.writeFile(`${dir}${req.body[userEntries[indexOfName]]}.xml`, xml)
  
  res.render('index')
})

app.listen(3000, () => {
  console.log('Expresss server running...')
})