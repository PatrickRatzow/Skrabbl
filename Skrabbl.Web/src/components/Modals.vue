<template>
    <div>
        <LoginModal v-if="isLoginModalVisible" />
        <RegisterModal v-else-if="isRegisterModalVisible" />
        <LogoutModal v-else-if="isLogoutModalVisible" />
        <RoundStatusModal />
    </div>
</template>

<script>
import { mapGetters } from "vuex";
import LoginModal from "@/components/modals/LoginModal";
import RegisterModal from "@/components/modals/RegisterModal";
import LogoutModal from "@/components/modals/LogoutModal";
import RoundStatusModal from "@/components/modals/RoundStatusModal";

export default {
    name: "Modals",
    components: {
        LogoutModal,
        LoginModal,
        RegisterModal,
        RoundStatusModal
    },
    computed: {
        ...mapGetters("user", {
            isLoginModalVisible: "isLoginModalVisible",
            isRegisterModalVisible: "isRegisterModalVisible",
            isLogoutModalVisible: "isLogoutModalVisible"
        })
    }
}
</script>

<style scoped>

</style>