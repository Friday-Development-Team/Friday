/**
 * File used to store models containing only data or very simple methods
 */

export class Item {
    constructor(public id: number, public name: string, public price: number, public type: string, public count: number, public itemDetails: ItemDetails, public logs: null, public normalizedImageName: string) { }
}

export class ItemDetails {
    constructor(public id: number, public itemId: number, public size: string, public calories: number, public sugarContent: number, public saltContent: number, public allergens: number) { }
}

export class Cart {
    total: number = 0
    items: OrderItem[] = []

    updateTotal() {
        this.total = this.items.map(s => { return s.item.price * s.amount }).reduce((acc, cur) => acc + cur)
    }
}

export class OrderItem {
    constructor(public item: Item, public amount: number) { }
}

export class OrderHistory {
    constructor(public orders?: HistoryOrder[], public username?: string) { }

    fromJson(json: any) {
        if (!json)
            return
        this.username = json.userName
        this.orders = json.orders.map(s => {
            let temp = new HistoryOrder()
            temp.fromJson(s)
            return s
        })

    }
}

export class HistoryOrder {

    constructor(public totalPrice?: number, public orderTime?: Date, public completionTimeFood?: Date,
        public completionTimeBeverage?: Date, public items?: HistoryOrderItem[]) { }

    fromJson(json: any) {
        this.totalPrice = json.totalPrice
        this.orderTime = json.orderTime
        this.completionTimeFood = json.completionTimeFood
        this.completionTimeBeverage = json.completionTimeBeverage
        this.items = json.items.map(s => {
            let item = new HistoryOrderItem()
            item.fromJson(s)
            return item
        })
    }
}
export class HistoryOrderItem {
    fromJson(json: any) {
        this.itemName = json.itemName
        this.amount = json.amount
    }
    constructor(public itemName?: string, public amount?: number) { }
}

export class CateringOrder {
    constructor(
        public id: number,
        public items: HistoryOrderItem[], public user: string,
        public statusFood: string,
        public statusBeverage: string,
        public orderTime: Date,
        public totalPrice: number) { }

    isActive(): boolean {
        const bool = this.statusBeverage === 'Accepted' || this.statusBeverage === 'Accepted' || this.statusFood === 'SentToKitchen'
        console.log(bool)
        return bool
    }
}

export class Log {
    constructor(public name: string, public amount: number, public time: Date, public type: string) { }
}

export class ItemAmount {
    constructor(public item: Item, public amount: number) { }
}

export class Configuration {
    constructor(public combinedCateringKitchen: boolean, public cancelOnAccepted: boolean) { }
}

export class ItemDTO {
    constructor(public name: string, public price: number, public type: string, public count: number, public normalizedImageName: string, public portionSize: string, public calories: number, public sugarContent: number, public saltContent: number, public allergens: string) { }
}
