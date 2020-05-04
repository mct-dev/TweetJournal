import Configuration from '../Configuration/Configuration.js';

/**
 *
 */
export class AuthenticationRequest {
    constructor({username = '', password = ''}) {
        this.Jwt = '';
        this.Errors = [];
        this.Successful = false;

        this._loginData = JSON.stringify({
            username,
            password,
        });
        this._requestConfig = {
            method: 'post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: this._loginData,
        };
    }

    async submit() {
        try {
            const url = `${Configuration.ApiBaseUrl}/api/v1/identity/login`;
            const response = await fetch(url, this._requestConfig);
            const jsonData = await response.json();

            if (!response.ok) {
                this.Successful = false;
                this.Errors = jsonData.errors;
                return;
            }

            this.Jwt = jsonData.token;
            this.Successful = true;
        } catch (err) {
            console.log('error caught');
            console.log(err);
            this.Successful = false;
            this.Errors.push(err.message);
        }
    }
}
