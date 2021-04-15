// Generic wrapper for https://developer.mozilla.org/en-US/docs/Web/API/Storage

class Storage {
    constructor(storageImplementation) {
        this.storage = storageImplementation
    }

    key(index) {
        return this.storage.key(index)
    }

    getItem(key) {
        return this.storage.getItem(key)
    }

    setItem(key, value) {
        this.storage.setItem(key, value)
    }
    
    removeItem(key) {
        this.storage.removeItem(key)
    }

    clear() {
        this.storage.clear()
    }
}

export default Storage