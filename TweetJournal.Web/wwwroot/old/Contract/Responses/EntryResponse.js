// eslint-disable-next-line require-jsdoc
export class EntryResponse {
  constructor ({ id = '', content = '', createdDate = '', modifiedDate = '' }) {
    this.id = id
    this.content = content
    this.createdDate = createdDate
    this.modifiedDate = modifiedDate
  }
}
