import Country from '@/models/Country'
import axios from 'axios'

export default {
    getAll: async () => {
        const response = await axios.get<Array<Country>>('https://restcountries.eu/rest/v2/all')
        return response.data
    }

}

