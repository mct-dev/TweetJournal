import Configuration from '../Configuration/Configuration.js';

// eslint-disable-next-line require-jsdoc
export class EntryAccess {
    /**
     * Create a new Entry.
     * @param {Entry} entry
     * @return {Promise<EntryResponse>}
     */
    async create(entry) {
        const url = `${Configuration.ApiBaseUrl}/api/v1/entry`;
        const requestConfig = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(entry),
        };
        const response = await fetch(url, requestConfig);
        const json = await response.json();
        if (!response.ok) {
            throw new Error(json.errors);
        }

        return json;
    }
}
