function authHeader(user){
    return {
        headers: { apiKey: user.token }
    };
}

export default authHeader;