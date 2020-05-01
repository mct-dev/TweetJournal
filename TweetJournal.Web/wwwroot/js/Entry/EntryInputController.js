import {Controller} from '../../web_modules/stimulus.js';

export default class EntryInputController extends Controller {
  static get targets() {
    return ['input', 'output'];
  }

  connect() {
    console.log('working!');
  }

  update() {
    this.outputTarget.textContent = this.inputTarget.value;
    fetch('https://localhost:5001/api/v1/entry', {
      method: 'post',
      body: {
        content: this.inputTarget.value,
      },
    });
  }
}
