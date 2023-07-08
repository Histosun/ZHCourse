import React from 'react';
import { Card } from 'antd';

const { Meta } = Card;

interface IndexCardProps {
    imgSrc: string,
    title: string,
    description: string
}

const IndexCard: React.FC<IndexCardProps> = (props: IndexCardProps) => {
    const onCardClick = () => {};
    return <Card
        hoverable
        style={{ width: 240 }}
        cover={<img alt="example" src={props.imgSrc} />}
        onClick={onCardClick}
    >
        <Meta title={props.title} description={props.description} />
    </Card>
};

export default IndexCard;