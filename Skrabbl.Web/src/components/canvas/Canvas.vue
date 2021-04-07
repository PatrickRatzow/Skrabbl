<template>
  <div class="p-3">
    <div>X: {{ mouseInfo.x }}, Y: {{ mouseInfo.y }}</div>
    <div>Connected to SignalR: {{ connected ? 'Yes' : 'No' }}</div>
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
        <li v-for="color of colors">
          <ColorButton 
              :key="color.backgroundColor"
              :name="color.name"
              :background-color="color.backgroundColor"
              :text-color="color.textColor || 'white'"
              @click="setColor"
          />
        </li>
      </ul>
    </div>
    <div class="mt-2">
      <ul class="is-flex">
        <li v-for="thickness of thicknesses">
          <SizeButton
              :key="thickness"
              :size="thickness"
              @click="setThickness"
          />
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import ColorButton from "@/components/canvas/ColorButton";
import SizeButton from "@/components/canvas/SizeButton";
export default {
  name: "Canvas",
  components: {
    ColorButton,
    SizeButton
  },
  data() {
    return {
      connection: null,
      connected: false,
      mouseInfo: {
        isDown: false,
        x: 0,
        y: 0
      },
      colors: [
        { name: "Black", backgroundColor: "black" },
        { name: "Red", backgroundColor: "#ff0000" },
        { name: "Blue", backgroundColor: "#0000ff" },
        { name: "Green", backgroundColor: "#00ff00" },
        { name: "Light Grey", backgroundColor: "#d5d5d5", textColor: "black" }
      ],
      thicknesses: [
          1,
          2,
          4,
          8,
          16,
          32,
          64
      ],
      stroke: {
        color: "black",
        thickness: 1
      },
      nodes: []
    }
  },
  mounted() {
    this.createConnection()
    this.setupHandlers()
    this.startConnection()
  },
  methods: {
    createConnection() {
      this.connection = new signalR.HubConnectionBuilder().withUrl("/ws/drawing-hub").build();
    },
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
    async startConnection() {
      await this.connection.start()
      
      this.connected = true
    },
    sendNode(node) {
      this.connection.invoke(
          "SendNode",
          node.color,
          node.size,
          ...node.position
      )
    },
    setColor(color) {
      this.stroke.color = color;
    },
    setThickness(thickness) {
      this.stroke.thickness = thickness;
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
  
  button {
    color: white;
    height: 40px;
    min-width: 70px;
    margin-right: 10px;
    font-size: 16px;
  }
</style>