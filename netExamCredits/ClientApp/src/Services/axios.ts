import axios, {AxiosRequestHeaders} from 'axios';

const axiosInstance = axios.create({
});

axiosInstance.interceptors.request.use(
    (config) => {
        return config;
    },
    (error) => Promise.reject(error)
);

export default axiosInstance;
