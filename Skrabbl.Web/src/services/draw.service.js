import ws from "../utils/signal-r"

class DrawService {
    sendColor({ backgroundColor}) {
        const r = parseInt(backgroundColor.slice(1, 3), 16);
        const g = parseInt(backgroundColor.slice(3, 5), 16);
        const b = parseInt(backgroundColor.slice(5, 7), 16);
        return ws.invoke("SendDrawNode", {
            color: [r, g, b]
        })
    }
}

export default new DrawService()