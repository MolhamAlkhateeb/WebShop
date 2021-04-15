import axios from 'axios';
export default class ProductService {
    async getProducts() {
        const response = await axios.get('/api/products');
        return response.data;
    }
}
//# sourceMappingURL=ProductService.js.map