import Authenticator from '../../services/authenticator'

class Controller {

    constructor($stateProvider, authenticator) {
		this.email = '';
		this.password = '';
		this.authenticator = authenticator;
    }

	clear() {
		this.message = '';
	}

	authenticate() {
		if (this.email == '' || this.password == '') {
			this.show("Please provide your email and password");
		} else {
			this.authenticateWithCredentials(this.email, this.password);
		}
	}

	/* Private */

	authenticateWithCredentials(email, password) {
		this.authenticator.authenticate(this.email, this.password);
		if (!this.authenticator.isAuthenticated) {
			this.show("Invalid credentials have been provided");
		} else {
			$stateProvider.state(stateName, stateConfig);
		}
	}

	show(message) {
		this.message = message;
	}
}

Controller.$inject = ['$stateProvider', ''];

export default Controller
