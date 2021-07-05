export default class Country {

    name = ""
    topLevelDomain?: string[]
    alpha2Code?: string
    alpha3Code?: string
    callingCodes?: string[]
    capital?: string
    altSpellings?: string[]
    region?: string
    subregion?: string
    population?: number
    latlng?: number[]
    demonym?: string
    area?: number
    gini?: number
    timezones?: string[]
    borders?: string[]
    nativeName?: string
    numericCode?: string
    currencies?: Currency[]
    languages?: Language[]
    translations?: any
    flag?: string
    cioc?: string
}

export class Currency {
    code?: string
    name?: string
    symbol?: string

}

export class Language {
    iso639_1?: string
    iso639_2?: string
    name?: string
    nativeName?: string
}