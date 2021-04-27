<template>
    <div class="rows">
        <SettingItem v-for="setting in settings"
                     :name="setting.name"
                     :value="setting.value" />
    </div>
</template>

<script>
    import SettingItem from "@/components/setting/SettingItem";
    export default {
        name: "SettingList",
        props: {
            settings: Array
        },
        components: {
            SettingItem
        }
    }
</script>

<style scoped>
</style>