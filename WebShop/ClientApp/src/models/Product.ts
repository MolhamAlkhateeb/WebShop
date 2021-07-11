import Image from './Image'
import PriceQuote from './PriceQuote'
import ProductSpecification from './ProductSpecification'
export default class Product {
    id?: number
    name?: string
    description?: string
    images?: Array<Image>
    prices?: Array<PriceQuote>
    itemNumber?: string
    specifications?: Array<ProductSpecification>
}