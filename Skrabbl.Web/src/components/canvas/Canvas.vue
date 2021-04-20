<template>
    <div>
        <div v-if="!isConnected">Is not connected to SignalR!</div>
        <canvas id="draw-canvas"
                height="500"
                width="500"
                @mousemove="draw"
                @mousedown="startDrawing"
                @mouseup="stopDrawing"
                @mouseout="stopDrawing" />
        <div>
            <ul class="is-flex">
                <li class="mr-2" v-for="color of colors">
                    <ColorButton :key="color.backgroundColor"
                                 :color="color" />
                </li>
            </ul>
        </div>
        <div class="mt-2">
            <ul class="is-flex">
                <li class="mr-2" v-for="size of sizes">
                    <SizeButton :key="size"
                                :size="size" />
                </li>
            </ul>
        </div>
    </div>
</template>

<script>
import { mapGetters, mapState } from "vuex"
import ColorButton from "@/components/canvas/ColorButton";
import SizeButton from "@/components/canvas/SizeButton";

export default {
        name: "Canvas",
        components: {
            ColorButton,
            SizeButton
        },
        computed: {
            ...mapState({
                isConnected: state => state.signalR.connected,
                sizes: state => state.canvas.sizes,
                colors: state => state.canvas.colors,
                connection: state => state.signalR.connection
            }),
            ...mapGetters("canvas", {
                size: "size",
                color: "color"
            }),
        },
        data() {
            return {
                mouseInfo: {
                    isDown: false,
                    x: 0,
                    y: 0,

                },
                nodes: [],
                canvasState: {
                    previousNodeEnd: null,
                    color: "000000",
                    thickness: 1,
                },
                hasSendStartNode: false
            }
        },
        mounted() {
            this.setupHandlers()
        },
        methods: {
            setupHandlers() {
                this.connection.on("ReceiveDrawNode", (command) => {
                    if (command.startNode || command.continueNode) {
                        let position;

                        if (command.startNode) {
                            position = command.startNode;
                            this.canvasState.previousNodeEnd = command.startNode.slice(2, 4);
                        } else if (command.continueNode) {
                            position = [
                                ...this.canvasState.previousNodeEnd,
                                ...command.continueNode
                            ];
                            this.canvasState.previousNodeEnd = command.continueNode;
                        }

                        this.drawLine({
                            position,
                            size: this.canvasState.thickness,
                            color: this.canvasState.color
                        });
                    } else if (command.color) {
                        const r = this.hexToDecimal(command.color[0])
                        const g = this.hexToDecimal(command.color[1])
                        const b = this.hexToDecimal(command.color[2])

                        this.canvasState.color = `#${r}${g}${b}`
                    } else if (command.thickness) {
                        this.canvasState.thickness = command.thickness;
                    }
                });
            },
            hexToDecimal(hex) {
                let num = hex.toString(16);
                if (num.length === 1) {
                    num = "0" + num
                }
                return num;
            },
            sendNode(node) {
                const command = {}
                if (!this.hasSendStartNode) {
                    this.hasSendStartNode = true;
                    command.startNode = node.position;
                } else {
                    command.continueNode = node.position.slice(2, 4);
                }

                this.connection.invoke(
                    "SendDrawNode",
                    command
                )
            },
            startDrawing(e) {
                this.mouseInfo.x = e.offsetX
                this.mouseInfo.y = e.offsetY
                this.mouseInfo.isDown = true
            },
            stopDrawing(e) {
                if (!this.mouseInfo.isDown)
                    return

                this.mouseInfo.x = 0
                this.mouseInfo.y = 0
                this.mouseInfo.isDown = false
                this.hasSendStartNode = false
            },
            draw(e) {
                if (!this.mouseInfo.isDown)
                    return

                const oX = e.offsetX
                const oY = e.offsetY
                const node = {
                    position: [this.mouseInfo.x, this.mouseInfo.y, oX, oY],
                    color: this.color.backgroundColor,
                    size: this.size,
                }
                this.sendNode(node)
                //this.drawLine(node)
                this.mouseInfo.x = oX
                this.mouseInfo.y = oY
            },
            drawLine(node) {
                const canvas = document.getElementById("draw-canvas")
                const ctx = canvas.getContext("2d")
                ctx.beginPath();
                ctx.strokeStyle = node.color;
                ctx.lineWidth = node.size;
                ctx.lineJoin = "round";
                ctx.lineCap = "round";
                ctx.moveTo(node.position[0], node.position[1]);
                ctx.lineTo(node.position[2], node.position[3]);
                ctx.stroke();
                ctx.closePath();
            },
        }
    }
</script>

<style scoped>
    canvas {
        border: 2px solid grey;
    }
</style>