import {useCookies} from "react-cookie";
import AuthService from "../services/AuthService.js";

const TokenValidation = () => {
    const [accessToken, setAccessToken] = useCookies('accessToken');
    const service = new AuthService();

    const parseJwt = (token) => {
        try {
            return JSON.parse(Buffer.from(token.split(".")[1], "base64").toString());
        } catch (e) {
            return null;
        }
    };

    setInterval(() => {
        if (accessToken.accessToken) {
            const decodedJwt = parseJwt(accessToken.accessToken);

            if (decodedJwt.exp * 1000 < Date.now()) {
                service.refreshToken();
            }
        }
    }, 5000);
}

export default TokenValidation;