import { JwtModuleOptions } from "@auth0/angular-jwt";
import { environment } from "src/environments/environment";
import { LocalStorageService } from "../../services/local-storage.service";

const JwtModuleOptionsConst = (): JwtModuleOptions => {
    var jwtModuleOptions: JwtModuleOptions = {
        config: {
            allowedDomains: environment.allowedDomains,
            disallowedRoutes: environment.blockedDomains,
            tokenGetter: (new LocalStorageService()).getToken
        }
    };

    return jwtModuleOptions;
};

export default JwtModuleOptionsConst;