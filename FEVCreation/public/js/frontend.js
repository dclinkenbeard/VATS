const submitButton = document.getElementById('submitButton')



submitButton.addEventListener('click', () => {
    // Inputs from form
    const neighborRadius = document.getElementById('neighborRadius')
    const avoidRadius = document.getElementById('avoidRadius')
    const collisionLength = document.getElementById('collisionLength')
    const minSpeed = document.getElementById('minSpeed')
    const maxSpeed = document.getElementById('maxSpeed')
    const minSize = document.getElementById('minSize')
    const maxSize = document.getElementById('maxSize')
    const minTemp = document.getElementById('minTemp')
    const maxTemp = document.getElementById('maxTemp')
    const minDepth = document.getElementById('minDepth')
    const maxDepth = document.getElementById('maxDepth')
    const lowerLimit = document.getElementById('lowerLimit')
    const upperLimit = document.getElementById('upperLimit')
    const fishType = document.getElementById('fishType')
    const modelUrl = document.getElementById('modelUrl')
    const name = document.getElementById('name')
    const scientificName = document.getElementById('scientificName')
    const type = document.getElementById('type')
    const diet = document.getElementById('diet')
    const habitat = document.getElementById('habitat')
    const range = document.getElementById('range')
    const status = document.getElementById('status')

    
    // Creating strings from user input and formatting according to VATS_Project/Assets/Resources/FEVs/EelSample.xml
    
    const xmlMinTemp = `<minTemp>${minTemp.value}</minTemp>`
    const xmlMaxTemp = `<maxTemp>${maxTemp.value}</maxTemp>`
    const xmlMinDepth = `<minDepth>${minDepth.value}</minDepth>`
    const xmlMaxDepth = `<maxDepth>${maxDepth.value}</maxDepth>`
    const xmlLowerLimit = `<lowerLimit>${lowerLimit.value}</lowerLimit>`
    const xmlUpperLimit = `<upperLimit>${upperLimit.value}</upperLimit>`
    const xmlFishType = `<fishType>${fishType.value}</fishType>`
    const xmlModelUrl = `<modelUrl>${modelUrl.value}</modelUrl>`
    const xmlName = `<name>${name.value}</name>`
    const xmlScientificName = `<scientificName>${scientificName.value}</scientificName>`
    const xmlType = `<type>${type.value}</type>`
    const xmlDiet = `<diet>${diet.value}</diet>`
    const xmlHabitat = `<habitat>${habitat.value}</habitat>`
    const xmlRange = `<range>${range.value}</range>`
    const xmlStatus = `<status>${status.value}</status>`


    
    
    // const xmlDoc = parser.parseFromString(xml, 'application/xml')
    const xmlInput = document.getElementById('xml')
    xmlInput.value = xml
})

// Generate id in index.js file and send data to fronend.js 

// store id in xmlDoc

