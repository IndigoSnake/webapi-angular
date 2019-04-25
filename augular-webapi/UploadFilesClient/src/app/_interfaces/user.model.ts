export interface User {
    id: string,
    name: string,
    address: string,
    imgPath: string
}

export interface  UploadFile {
    name: string,
    type: string,
    size: string,
    uploadedDate: string,
    user: string
}
