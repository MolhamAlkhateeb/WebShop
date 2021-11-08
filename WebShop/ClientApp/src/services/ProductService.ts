import erpClient from "./ErpServiceClient"
import Product from '../models/Product'
import authService from "@/components/api-authorization/AuthorizeService"

export default class ProductService {

    async getProducts() {
        const token = authService.getAccessToken();
        const response = await erpClient.get<Array<Product>>('/api/products', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        })
        return response.data
    }

    async getProduct(id: number) {
        const response = await erpClient.get<Product>(`/api/products/${id}`)
        return response.data
    }
}