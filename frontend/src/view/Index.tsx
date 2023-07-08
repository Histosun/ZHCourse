import React from 'react';
import { Col, Row } from 'antd';
import IndexCard from '../component/IndexCard';

const Index: React.FC = () => {
    return (
        <Row justify="space-around">
            <Col span={4}>
                <IndexCard />
            </Col>
        </Row>
    )
}

export default Index;