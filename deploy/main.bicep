targetScope = 'subscription'

var fakeProductsGroupName = 'rg-fake-products'
var fakeProductsModuleName = 'fake-products-collection'

resource fakeProductsGroup 'Microsoft.Resources/resourceGroups@2024-03-01' = {
  name: fakeProductsGroupName
  location: deployment().location
}

module fakeProductsModule 'fake-products-collection.bicep' = {
  scope: fakeProductsGroup
  name: fakeProductsModuleName
}