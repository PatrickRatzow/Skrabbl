<template>
  <div>
    <div>X: {{ mouseInfo.x }}, Y: {{ mouseInfo.y }}</div>
    <div>Connected to SignalR: {{ isConnected ? 'Yes' : 'No' }}</div>
    <div>Color: {{ color.name }}</div>
    <div>Size: {{ size }}</div>
    <canvas id="draw-canvas"
            height="500"
            width="500"
            @mousemove="draw"
            @mousedown="startDrawing"
            @mouseup="stopDrawing"
            @mouseout="stopDrawing"
    />
    <div>
      <ul class="is-flex">
        <li class="mr-2" v-for="color of colors">
          <ColorButton
              :key="color.backgroundColor"
              :color="color"
          />
        </li>
      </ul>
    </div>
    <div class="mt-2">
      <ul class="is-flex">
        <li class="mr-2" v-for="size of sizes">
          <SizeButton
              :key="size"
              :size="size"
          />
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapState} from "vuex"
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
      colors: state => state.canvas.colors
    }),
    ...mapGetters("canvas", {
      size: "size",
      color: "color"
    })
  },
  data() {
    return {
      mouseInfo: {
        isDown: false,
        x: 0,
        y: 0
      },
      nodes: []
    }
  },
  methods: {
    setupHandlers() {
      this.connection.on("ReceiveNode", (color, size, x1, y1, x2, y2) => {
        this.addNode(color, size, x1, y1, x2, y2);
        this.drawLine({
          position: [x1, y1, x2, y2],
          size,
          color
        });
      });
    },
    sendNode(node) {
      this.connection.invoke(
          "SendNode",
          node.color,
          node.size,
          ...node.position
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
    },
    draw(e) {
      if (!this.mouseInfo.isDown)
        return
      
      const oX = e.offsetX
      const oY = e.offsetY
      const node = this.addNode(this.stroke.color, this.stroke.thickness, this.mouseInfo.x, this.mouseInfo.y, oX, oY)
      this.sendNode(node)
      this.drawLine(node)
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
    addNode(color, size, x1, y1, x2, y2) {
      const node = {
        position: [ x1, y1, x2, y2 ],
        color,
        size,
      }
      this.nodes.push(node)
      
      return node
    }
  }
}
</script>

<style scoped>
  canvas {
    border: 2px solid grey;
  }
</style>