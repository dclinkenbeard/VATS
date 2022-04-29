const { promises : fs } = require('fs')
const path = require('path')


// Uses fs to count the number of xml files in /FEVs and calculates the id for the new FEV
const generateId = async (dir) => {
  const files = await fs.readdir(dir)
  let id = 0

  // returns the number of files that end in .xml
  for (file of files) {
    // https://www.geeksforgeeks.org/node-js-fs-readdir-method/
    if (path.extname(file) == ".xml")
      id++
  }

  return ++id
}

module.exports = createXmlString = async (req, entries, dir) => {
  // Formatting xml tags with appropriate values
  let xml = '<?xml version="1.0" encoding="UTF-8" standalone="yes" ?>\n'
  xml += '<FEV>\n'

  // Generating tags
  for (entry of entries)
    xml += `\t<${entry}>${req.body[entry]}</${entry}>\n`

  let id = await generateId(dir)
  xml += `\t<id>${id}</id>\n`
  xml += '</FEV>\n'

  return xml
}