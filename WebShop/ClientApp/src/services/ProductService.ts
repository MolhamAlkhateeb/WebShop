import axios from 'axios'
import Product from '../models/Product'

export default class ProductService {

    async getProducts() {
        const response = await axios.get<Array<Product>>('/api/products')
        return response.data
    }

    async getProduct(id: number) {
        const response = await axios.get<Product>(`/api/products/${id}`)
        return response.data
    }
}