$apiName = 'Weather'
$swaggerUrl = 'http://localhost:5000/swagger/v1/swagger.json'
$apiNamespace = 'WebApiClientTestApp.Client'

import-module ..\ApiHelpers\ClientGeneration.psm1 -Force

GenerateClient $swaggerUrl $apiNamespace $apiName

