import {Application} from '/web_modules/stimulus.js';
import EntryInputController from '/js/Entry/EntryInputController.js';
import AuthenticationController
    from './Authentication/AuthenticationController.js';

const application = Application.start();
application.register('entryinput', EntryInputController);
application.register('authentication', AuthenticationController);
