import erpClient from "./ErpServiceClient"
import Product from '../models/Product'

export default class ProductService {

    async getProducts() {
        const response = await erpClient.get<Array<Product>>('/api/products')
        return response.data
    }

    async getProduct(id: number) {
        const response = await erpClient.get<Product>(`/api/products/${id}`)
        return response.data
    }
}