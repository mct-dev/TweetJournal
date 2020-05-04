import {Controller} from '../../web_modules/stimulus.js';
import {AuthenticationRequest} from '../Authentication/AuthenticationRequest.js';

// eslint-disable-next-line require-jsdoc
export default class extends Controller {
    static get targets() {
        return ['username', 'password'];
    }

    async submit() {
        const authData = {
            username: this.username,
            password: this.password,
        };
        const authenticationRequest = new AuthenticationRequest(authData);
        await authenticationRequest.submit();

        if (!authenticationRequest.Successful) {
            console.log('unable to log in');
            return;
        }

        console.log('successful login!');
        console.log(`token: ${authenticationRequest.Jwt}`);
    }

    get username() {
        return this.usernameTarget.value;
    }

    get password() {
        return this.passwordTarget.value;
    }
}
