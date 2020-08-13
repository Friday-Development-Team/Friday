export class PriceFilter {

    constructor(private type: string, private amount: number) { }

    compareTo(a: number): boolean {
        switch (this.type) {
            case '<':
                return a < this.amount
            case '<=':
                return a <= this.amount
            case '>':
                return a > this.amount
            case '>=':
                return a >= this.amount
            case 'none':
                return true;
            default:
                return false;
        }

    }

}