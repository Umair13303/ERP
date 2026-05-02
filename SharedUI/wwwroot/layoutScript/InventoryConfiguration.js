//class IProductConfig {
//    constructor(data = {}) {
//        this.MachineNumberConf = data.machineNumberConf || { display: 'none' };
//        this.SKUConf = data.sKUConf || { display: 'none' };
//        this.AttributeConf = data.attributeConf || { display: 'none' };
//        this.FavoriteConf = data.favoriteConf || { display: 'none' };
//        this.SaleTaxConf = data.saleTaxConf || { display: 'none' };
//        this.ExpiryConf = data.expiryConf || { display: 'none' };
//        this.ATIConf = data.aTIConf || { display: 'none' };
//    }
//}

//class DocumentConfiguration {
//    constructor() {
//        this.productSettings = null;
//    }
//    async fetchAndSetProductSettings() {
//        try {
//            const response = await fetch(window.basePath+ 'Inventory/IProductManagement/fetchProductSettingByClientKEY');
//            if (!response.ok) throw new Error("Failed to fetch settings");
//            const data = await response.json();
//            this.productSettings = new IProductConfig(data);
//            this.applyIProductConfigs();
//            console.log(data)
//        }
//        catch (error) {
//        }
//    }
//}