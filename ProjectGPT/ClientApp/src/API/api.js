import axios from 'axios'

const instance = axios.create({
    baseURL: 'https://localhost:7261'
})

const CreateChat = (model) => {
    const url = `/CreateConversation`;
    return instance.post(url, { model }, {
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(error => {
            console.log(error);
        })
};

const CreateChatWithBehavior = (behavior, model) => {
    const url = `/CreateConversationWithBehavior`;
    return instance.post(url, { behavior, model }, {
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(response => {
            return response.data;
        })
        .catch(error => {
            console.log(error);
        })
};

const getConversations = () => {
    const url = '/GetConversations';
    return instance.get(url)
        .then(response => {
            return response.data;
        })
        .catch(error => {
            console.log(error);
        })
};

const getAnswer = (text) => {
    const url = '/GetAnswer';
    return instance.post(url, { text })
        .then(response => {
            return response.data;
        })
        .catch(error => {
            console.log(error);
        })
};

const loadConversation = (id) => {
    const url = `/LoadConversation?id=${id}`;
    return instance.get(url)
        .then(response => {
            const { data } = response
            const newData = []
            let conversation = {}
            data.forEach(object => {

                if (Object.entries(conversation).length === 2) {
                    newData.push(conversation)
                    conversation = {}
                    id = object.id
                }
                conversation[object.sender] = object.text
            })
            newData.push(conversation)
            return newData;
        })
        .catch(error => {
            console.log(error);
        })
};

const updateConversation = (id, name, model, chat) => {
    const url = `/UpdateConversation?Id=${id}&Name=${name}&Model=${model}&Chat=${chat}`;
    return instance.get(url)
        .then(response => {
            return response.data
        })
        .catch(error => {
            console.log(error);
        })
};

const deleteConversation = (id) => {
    const url = `/DeleteConversation?id=${id}`;
    return instance.delete(url)
        .then(response => {
            return response.data;
        })
        .catch(error => {
            console.log(error);
        })
};

export { CreateChat, CreateChatWithBehavior, getConversations, deleteConversation, updateConversation, loadConversation, getAnswer };