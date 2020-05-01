import {h, render} from '/js/web_modules/preact.js';
import htm from '/js/web_modules/htm.js';
const html = htm.bind(h);

function SomePreactComponent(props) {
  return html`<h1 style="color: red">Hello, World!</h1>`;
}

render(html`<${SomePreactComponent} />`, document.getElementById('app'));
