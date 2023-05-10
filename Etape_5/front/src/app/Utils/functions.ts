export function removeEmptyProperty(object: any) {
    const output: any = {};
    const array = Object.keys(object).map((key) => [key, object[key]]);
    for (const key of array) {
        if (key[1] !== '') {
            output[key[0]] = key[1];
        }
    }
    return output;
}

export function changeProperty(object: any, target: string, newName: string) {
    const output: any = {};
    const array = Object.keys(object).map((key) => [key, object[key]]);
    for (const key of array) {
        if (key[0] === target) {
            key[0] = newName;
        }
        output[key[0]] = key[1];
    }
    return output;
}