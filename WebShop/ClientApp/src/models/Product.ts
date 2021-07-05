import Image from './Image'
import PriceQuote from './PriceQuote'
export default class Product {
    id?: number
    name?: string
    description?: string
    images?: Array<Image>
    prices?: Array<PriceQuote>

}