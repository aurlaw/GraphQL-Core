

export const convertToDate = (str) => {
    return new Date(str);
}

export const formatDateFromString = (str) => {
    let d = convertToDate(str);
    if(d !== null) {
        return d.toLocaleString('en-US');
    }
    return '';

}

export const formatDate = (date) => {
    return date.toLocaleString('en-US');

}