import axios from 'axios';

const API_URL = 'https://localhost:7251/api/Login';

export interface LoginCredentials {
    login: string;
    password: string;
}

export interface AuthResponse {
    token: string;
    roleId: string;
}

class AuthService {
    async login(credentials: LoginCredentials): Promise<AuthResponse> {
        const response = await axios.post<AuthResponse>(API_URL, credentials);
        if (response.data.token) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('roleId', response.data.roleId);
        }
        return response.data;
    }

    logout(): void {
        localStorage.removeItem('token');
        localStorage.removeItem('roleId');
    }

    isLoggedIn(): boolean {
        return !!localStorage.getItem('token');
    }

    getToken(): string | null {
        return localStorage.getItem('token');
    }

    getRoleId(): string | null {
        return localStorage.getItem('roleId');
    }
}

export default new AuthService(); 