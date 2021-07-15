import { Vue } from "vue-class-component";



export default class ValidationProvider<T extends Vue> {
    component: T;
    schema: any;
    source: string;
    fields: any = {}
    constructor(component: T, schema: any, source: any) {
        this.component = component;
        this.schema = schema;
        this.source = source;
        this.setupWatcher();
        for (const key in schema) {
            this.fields[key] = []
        }
    }

    setupWatcher() {
        this.component.$watch(this.source, this.watchHandler, { deep: true })
    }

    watchHandler = (newValue: any) => {
        this.refreshValidation(newValue)
    }

    refreshValidation(val: any) {
        for (const key in this.schema) {
            const validators = this.schema[key]
            this.fields[key] = [];
            for (const validator of validators) {
                const errorMsg = validator(val[key], key, this.component)
                if (errorMsg) {
                    this.fields[key].push(errorMsg);
                }
            }
        }
    }

    errorsFor(field: string): string[] {
        return this.fields[field] || []
    }

    isValid() {
        for (const key in this.fields) {
            if (!this.isFieldValid(key)) {
                return false;
            }
        }
        return true;
    }

    isFieldValid(name: string) {
        return this.fields[name]?.length < 1
    }
}

export const required = (val: any, field: string) => {
    if (!val) {
        return 'This field is required'
    }
}

export const email = (val: any, field: any) => {
    if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(val)) {
        return "This field must be a valid email";
    }
}

export const sameAs = (property: string) => {

    const getNestedObject = (nestedObj: any, path: any) => {
        return path.split(".").reduce((obj: any, key: any) =>
            (obj && obj[key] !== 'undefined') ? obj[key] : undefined, nestedObj);
    }
    return (val: any, field: any, component: any) => {
        if (val !== getNestedObject(component, property)) {
            return `This field must match the ${property.split('.').reverse()[0]} field`;
        }
    }
}

export const min = (n: number) => {
    return (val: any) => {
        if (val < n) {
            return `Minimum value allowed ${n}`
        }
    }
}

export const max = (n: number) => {
    return (val: any) => {
        if (val > n) {
            return `Maximum value allowed ${n}`
        }
    }
}

export const minLength = (n: number) => {
    return (val: any) => {
        if (val?.length < n) {
            return `Minimum length allowed ${n}`
        }
    }
}

export const maxLength = (n: number) => {
    return (val: any) => {
        if (val?.length > n) {
            return `Maximum length allowed ${n}`
        }
    }
}


