import React from 'react';

import './deleteBtn.css';


export const DeleteBtn = (props) => (
    <button className="btn btn-danger p-del" onClick={e => {
        e.preventDefault();
        if(window.confirm(props.message)) 
        {
            props.onDelete();
        }
    }}>X</button>
);