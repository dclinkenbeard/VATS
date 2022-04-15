const submitButton = document.getElementById('submitButton')

submitButton.addEventListener('click', () => {
    // Inputs from form
    const neighborRadius = document.getElementById('neighborRadius')
    const avoidRadius = document.getElementById('avoidRadius')
    const collisionLength = document.getElementById('collisionLength')
    const minSpeed = document.getElementById('minSpeed')
    const maxSpeed = document.getElementById('maxSpeed')
    const id = document.getElementById('id')
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
    const xmlNeighborRadius = `<neighborRadius>${neighborRadius.innerHTML}</neighborRadius>`
    const xmlAvoidRadius = `<avoidRadius>${avoidRadius.innerHTML}</avoidRadius>`
    const xmlCollisionLength = `<collisionLength>${collisionLength.innerHTML}</collisionLength>`
    const xmlMinSpeed = `<minSpeed>${minSpeed.innerHTML}</minSpeed>`
    const xmlMaxSpeed = `<maxSpeed>${maxSpeed.innerHTML}</maxSpeed>`
        // skipping id since that will be generated from the number of files in FEVs folder
    const xmlMinSize = `<minSize>${minSize.innerHTML}</minSize>`
    const xmlMaxSize = `<maxSize>${maxSize.innerHTML}</maxSize>`
    const xmlMinTemp = `<minTemp>${minTemp.innerHTML}</minTemp>`
    const xmlMaxTemp = `<maxTemp>${maxTemp.innerHTML}</maxTemp>`
    const xmlMinDepth = `<minDepth>${minDepth.innerHTML}</minDepth>`
    const xmlMaxDepth = `<maxDepth>${maxDepth.innerHTML}</maxDepth>`
    const xmlLowerLimit = `<lowerLimit>${lowerLimit.innerHTML}</lowerLimit>`
    const xmlUpperLimit = `<upperLimit>${upperLimit.innerHTML}</upperLimit>`
    const xmlFishType = `<fishType>${fishType.innerHTML}</fishType>`
    const xmlModelUrl = `<modelUrl>${modelUrl.innerHTML}</modelUrl>`
    const xmlName = `<name>${name.innerHTML}</name>`
    const xmlScientificName = `<scientificName>${scientificName.innerHTML}</scientificName>`
    const xmlType = `<type>${type.innerHTML}</type>`
    const xmlDiet = `<diet>${diet.innerHTML}</diet>`
    const xmlHabitat = `<habitat>${habitat.innerHTML}</habitat>`
    const xmlRange = `<range>${range.innerHTML}</range>`
    const xmlStatus = `<status>${status.innerHTML}</status>`
 })