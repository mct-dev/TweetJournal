import {Application} from '/web_modules/stimulus.js';
import EntryInputController from './Controllers/EntryInputController.js';
import AuthenticationController
    from './Controllers/AuthenticationController.js';

const application = Application.start();
application.register('entryinput', EntryInputController);
application.register('authentication', AuthenticationController);
