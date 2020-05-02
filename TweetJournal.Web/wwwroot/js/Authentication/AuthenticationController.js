import {Controller} from '../../web_modules/stimulus.js';

export default class extends Controller {
    static get targets() {
        return ['username', 'password'];
    }

    submit() {
        fetch('https://localhost:5555/api/v1/identity/login', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                username: this.usernameTarget.value,
                password: this.passwordTarget.value,
            }),
        }).then((response) => {
            console.log(response);
        }).catch((err) => {
            console.log(err);
        });
    }
}
