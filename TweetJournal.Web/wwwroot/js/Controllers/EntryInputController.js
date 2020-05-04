import {Controller} from '../../web_modules/stimulus.js';
import {EntryAccess} from '../Access/EntryAccess.js';
import {Entry} from '../Domain/Entry.js';

const PREFIX_KEY_DATA_ATTRIBUTE = 'pressedPrefixKey';

// eslint-disable-next-line require-jsdoc
export default class EntryInputController extends Controller {
    static get targets() {
        return ['input', 'output'];
    }

    connect() {
        console.log('entryinput connected!');
    }

    async update() {
        const entryAccess = new EntryAccess();
        try {
            const newEntry = new Entry(this.inputValue);
            const entryResponse = await entryAccess.create(newEntry);
            this.output.textContent = entryResponse;
        } catch (err) {
            console.log(err);
        }
    }

    keydown(e) {
        const allowedPrefixKeys = [
            'Shift',
        ];
        const currentlyPressedKey = this.data.get(PREFIX_KEY_DATA_ATTRIBUTE);

        // if prefix key is still pressed
        if (currentlyPressedKey) {
            const keyCombo = `${currentlyPressedKey} + ${e.key}`;
            console.log(keyCombo);
            this._setPrefixKey();
            return;
        }

        if (allowedPrefixKeys.includes(e.key)) {
            this._setPrefixKey(e.key);
            return;
        }
    }

    keyup(e) {
        const currentlyPressedKey = this.data.get(PREFIX_KEY_DATA_ATTRIBUTE);
        console.log('keyup.  pressed: ', currentlyPressedKey);
    }

    _setPrefixKey(value = '') {
        this.data.set(PREFIX_KEY_DATA_ATTRIBUTE, value);
    }

    get inputValue() {
        return this.inputTarget.value;
    }

    get output() {
        return this.outputTarget;
    }
};
