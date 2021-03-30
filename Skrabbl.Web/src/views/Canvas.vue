<template>
    <div id="canvas-wrapper">
        <div>
            <span>{{x}}, {{y}},</span>
            <h1>Drawing with mousemove event</h1>
            <canvas id="myCanvas" height="500" width="500" @mousemove="draw" @mousedown="beginDrawing" @mouseup="stopDrawing" @mouseout="stopDrawing"></canvas>
        </div>
        <ul class="is-flex">
            <li v-for="color in colors">
                <button @click="changeStrokeColor(color)" :style="buttonColor(color)">
                    {{color}}
                </button>
            </li>
        </ul>
        <ul class="is-flex mt-2">
            <li v-for="thickness in thicknesses">
                <button @click="changeStrokeThickness(thickness)">
                    {{thickness}}
                </button>
            </li>
        </ul>

        <div>
            <ul>

                <li v-for="message in messages">
                    {{message}}
                </li>

            </ul>
            <ul>
                <li>
                    <input type="text" v-model="currentMessage">

                    </input>
                    <button @click="sendMessage">
                        send message
                    </button>

                </li>
            </ul>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                colors: ['blue', 'red', 'yellow', 'brown', 'purple', 'pink', 'black', 'white'],
                thicknesses: [1, 2, 3, 5, 8],
                x: 0,
                y: 0,
                isDrawing: false,
                strokeColor: 'black',
                strokeThickness: 1,
                messages: ['hej', 'med'],
                currentMessage: ``
            }
        },
        mounted() {
        },
        methods: {
            drawLine(x1, y1, x2, y2) {
                let c = document.getElementById("myCanvas");
                this.canvas = c.getContext('2d');

                let ctx = this.canvas;
                ctx.beginPath();
                ctx.strokeStyle = this.strokeColor;
                ctx.lineWidth = this.strokeThickness;
                ctx.moveTo(x1, y1);
                ctx.lineTo(x2, y2);
                ctx.stroke();
                ctx.closePath();
            },
            draw(e) {
                if (this.isDrawing) {
                    this.drawLine(this.x, this.y, e.offsetX, e.offsetY);
                    this.x = e.offsetX;
                    this.y = e.offsetY;
                }
            },
            beginDrawing(e) {
                this.x = e.offsetX;
                this.y = e.offsetY;
                this.isDrawing = true;
            },
            stopDrawing(e) {
                if (this.isDrawing) {
                    this.drawLine(this.x, this.y, e.offsetX, e.offsetY);
                    this.x = 0;
                    this.y = 0;
                    this.isDrawing = false;
                }
            },
            buttonColor(color) {
                return `background-color: ${color}`
            },
            changeStrokeColor(color) {
                this.strokeColor = color;
            },
            changeStrokeThickness(thickness) {
                this.strokeThickness = thickness;
            },
            sendMessage() {


                if (this.currentMessage !== '') {
                    this.messages.push(this.currentMessage);
                    this.currentMessage = '';
                }
            }
        }
    }
</script>

<style scoped>
    #canvas-wrapper {
        padding: 6px;
    }

    canvas {
        border: 2px solid grey;
    }

    button {
        color: white;
        height: 40px;
        width: 70px;
        margin-right: 10px;
        font-size: 16px;
        -webkit-text-stroke: 0.5px black;
    }

    li {
    }
</style>