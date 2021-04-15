<template>
    <div>
        <LoginModal v-if="isLoginModalVisible" />
        <RegisterModal v-else-if="isRegisterModalVisible" />
        <LogoutModal v-else-if="isLogoutModalVisible" />
        <ChooseWordModal v-else-if="!isWordListEmpty"/>

        <RoundStatusModal />  
    </div>
</template>

<script>
import { mapGetters } from "vuex";
import LoginModal from "@/components/modals/LoginModal";
import RegisterModal from "@/components/modals/RegisterModal";
import LogoutModal from "@/components/modals/LogoutModal";
import ChooseWordModal from "@/components/modals/ChooseWordModal";
import RoundStatusModal from "@/components/modals/RoundStatusModal";

export default {
    name: "Modals",
    components: {
        LogoutModal,
        LoginModal,
        RegisterModal,
        ChooseWordModal,
        RoundStatusModal
    },
    computed: {
        ...mapGetters("user", {
            isLoginModalVisible: "isLoginModalVisible",
            isRegisterModalVisible: "isRegisterModalVisible",
            isLogoutModalVisible: "isLogoutModalVisible",
        }),
        ...mapGetters("game", {
            isWordListEmpty: "isWordListEmpty"
        })
    }
}
</script>

<style scoped>

</style>