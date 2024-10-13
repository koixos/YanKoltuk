import React from "react";
import "./ViewList.css"

function ViewList({ items, onItemClick }) {
    return (
        <div class="container" id="viewlist-container">
            <div class="items" id="viewlist-items">
                <div class="items-head" id="viewlist-items-head">
                    <p>Kayıtlı Servisler</p>
                    <hr/>
                </div>
                <div class="items-body" id="viewlist-items-body">
                    {
                        items.map((item) => (
                            <div 
                                key={item.id}
                                onClick={() => onItemClick(item)}
                                class="items-body-content"
                                id="viewlist-items-body-content"
                            >
                                <span> {item.plate} - {item.driver.name} </span>
                                <i class="fa fa-angle-right"></i>
                            </div>
                        ))
                    }
                </div>
            </div>
        </div>
    );
};

export default ViewList;