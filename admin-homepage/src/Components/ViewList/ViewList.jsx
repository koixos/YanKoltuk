import React from "react";
import "./ViewList.css"

function ViewList({ items }) {
    return (
        <div class="container" id="viewlist-container">
            <div class="items" id="viewlist-items">
                <div class="items-head" id="viewlist-items-head">
                    <p>Kayıtlı Servisler</p>
                    <hr/>
                </div>
                <div class="items-body" id="viewlist-items-body">
                    {
                        items.map((item, i) => (
                            <div key={i} class="items-body-content" id="viewlist-items-body-content">
                                <span> {item.plate} </span>
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