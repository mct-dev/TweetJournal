import {Application} from '/web_modules/stimulus.js';
import EntryInputController from '/js/Entry/EntryInputController.js';

const application = Application.start();
application.register('entryinput', EntryInputController);
